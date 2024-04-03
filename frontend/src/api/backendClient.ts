import Sighting from "../models/request/AddSightingRequest"
import Reaction from "../models/request/ReactionRequest"

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

export const getSightings = async (token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings`, {
    method: "get",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}
export const getSightingById = async (id?: string, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/${id}`, {
    method: "get",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  })
}

export const addReaction = async (reaction: Reaction, token?: string) => {
  const reactionRequest = {
    type: reaction.reactionType,
    sightingId: reaction.sightingId,
  }
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/reactions`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(reactionRequest),
  })
}

export const updateReaction = async (reaction: Reaction, token?: string) => {
  const newReactionRequest = {
    type: reaction.reactionType,
    sightingId: reaction.sightingId,
  }
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/reactions`, {
    method: "patch",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(newReactionRequest),
  })
}

export const deleteReaction = async (sightingId: number, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/reactions`, {
    method: "delete",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({ sightingId: sightingId }),
  })
}

export const getHotSpotById = async (id?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/hotspots/${id}`)
}
