import axios from "axios";
import { useAccountStore } from "@/stores/accountStore";

export class Request {
  constructor(controller) {
    this.controller = controller;
  }

  endpoint = (action) => `${import.meta.env.VITE_FOODVAULT_API_URL}/${this.controller}/${action}`;

  options = (method, data, headers) => {
    if (!data) {
      return {
        method: method
      }
    }
    if (method === "GET" || method === "DELETE") {
      return {
        method: method,
        params: data
      }
    }
    return {
      method: method,
      data: data
    }
  }

  makeRequest = async (action, method, body = null) => {
    let config = {
      method: method
    }
    return (await axios.request({
      ...config,
      url: this.endpoint(action),
      data: body
    })).data;
  }

  makeAuthenticatedRequest = async function(action, method, body = null) {
    let config = {
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("fv-token")}`
      },
      method: method
    }
    return (await axios.request({
      ...config,
      url: this.endpoint(action),
      data: body
    }).catch((error) => {
      console.error(error);
      if (error.response.status === 401) {
        useAccountStore().logOut();
      }
    })).data;
  }
}