import axios from "axios";

var loginRequest = function(data, callback) {
  console.log(JSON.stringify(data));
  return axios
    .post(import.meta.env.VITE_FOODVAULT_API_URL + "/User/Authenticate", data)
    .then((response) => response.data)
    .then((data) => callback(data));
}

var signupRequest = function(data, callback) {
  return axios
    .post(import.meta.env.VITE_FOODVAULT_API_URL + "/User/SignUp", data)
    .then((response) => response.data)
    .then((data) => callback(data));
}

var getUser = function(userId, callback) {
  const config = {
    headers: {
      "Authorization": `Bearer ${localStorage.getItem("fv-token")}`
    }
  }
  return axios
    .get(import.meta.env.VITE_FOODVAULT_API_URL + `/User/${userId}`, config)
    .then((response) => response.data)
    .then((data) => callback(data));
}

export {
  loginRequest,
  signupRequest,
  getUser
}