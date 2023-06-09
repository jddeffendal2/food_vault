import axios from "axios";
import { useAccountStore } from "@/stores/accountStore";

export class Request {
  constructor(controller) {
    this.controller = controller;
  }

  endpoint = (action) => `${import.meta.env.VITE_FOODVAULT_API_URL}/${this.controller}/${action}`;

  options = (method, data, headers) => {
    let options = {
      method: method,
    };

    if (headers) options.headers = headers;

    if (!data) return options;

    if (method === "GET" || method === "DELETE") {
      return {
        ...options,
        params: data,
      };
    }
    return {
      ...options,
      data: data,
    };
  }

  makeAnonymousRequest = async (action, method, body = null) => {
    let config = {
      method: method
    }
    return (await axios.request({
      ...config,
      url: this.endpoint(action),
      data: body
    })).data;
  }

  makeRequest = async function (action, method, body = null) {
    let config = this.options(method, body, {
      Authorization: `Bearer ${localStorage.getItem("fv-token")}`,
    });
    return (
      await axios
        .request({
          ...config,
          url: this.endpoint(action),
        })
        .catch((error) => {
          console.error(error);
          if (error.response.status === 401) {
            useAccountStore().logOut();
          }
        })
    ).data;
  };
}
