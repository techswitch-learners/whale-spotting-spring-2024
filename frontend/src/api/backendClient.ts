import Sighting from "../models/request/AddSightingRequest"
import Reaction from "../models/request/ReactionRequest"
import VerifySightingRequest from "../models/request/VerifySightingRequest"

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

export const getHotspotById = async (id?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/hotspots/${id}`)
}

export const getAchievements = async () => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/achievements`)
}

export const getHotspots = async (searchQuery: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/hotspots?${searchQuery}`)
}

export const getMonths = async () => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/months`)
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

export const resetProfilePicture = async (userName?: string, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/users/${userName}/profile-image`, {
    method: "delete",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
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

export const verifySighting = async (
  verifySightingRequest: VerifySightingRequest,
  sightingId: number,
  token?: string,
) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/${sightingId}/verify`, {
    method: "post",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(verifySightingRequest),
  })
}

export const editApprovalStatus = async (sightingId?: number, token?: string) => {
  return await fetch(`${import.meta.env.VITE_BACKEND_URL}/sightings/${sightingId}/verification`, {
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
