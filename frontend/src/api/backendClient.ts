import Sighting from "../models/request/AddSightingRequest"

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

export const addSighting = async (sighting: Sighting, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(sighting),
  })
}

export const getSpeciesList = async () => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/species`)
}

export const getSightings = async () => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings`)
}

export const getSightingById = async (id?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/${id}`)
}

export const getHotSpotById = async (id?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/hotspots/${id}`)
}

export const getUser = async (token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/users/current`, {
    method: "get",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}
