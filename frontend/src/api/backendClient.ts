import Sighting from "../models/sighting"

export const loginUser = async (username: string, password: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/auth/login`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username,
      password,
    }),
  })
}

export const registerUser = async (username: string, password: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/auth/register`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username,
      password,
    }),
  })
}

export const addSighting = async (sighting: Sighting) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sighting/add`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(sighting),
  })
}
