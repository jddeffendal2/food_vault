import axios from "axios";

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_FOODVAULT_API_URL,
  headers: { "Authorization": `Bearer ${localStorage.getItem("token")}`}
})

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

export {
  loginRequest,
  signupRequest
}