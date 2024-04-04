import Sighting from "../models/request/AddSightingRequest"
import VerificationEvent from "../models/request/VerificationEvent"

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

export const getUsers = async (token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/users/all`, {
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}

export const deleteUser = async (username?: string, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/users/${username}`, {
    method: "delete",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}

export const editProfilePicture = async (userName?: string, token?: string, url?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/users/edit/${userName}`, {
    method: "patch",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(url),
  })
}

export const getPendingSightings = async (token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/pending`, {
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}

export const getRejectedSightings = async (token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/rejected`, {
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}

export const verifySighting = async (verificationEvent: VerificationEvent, sightingId: number, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/${sightingId}/verify`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(verificationEvent),
  })
}

export const editApprovalStatus = async (sightingId?: number, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/edit/${sightingId}`, {
    method: "patch",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}

export const deleteSighting = async (sightingId?: number, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/${sightingId}`, {
    method: "delete",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}
