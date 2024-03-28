import ReactionType from "../../types/ReactionType"

interface ReactionRequest {
  reactionType: ReactionType
  sightingId: number
}

export default ReactionRequest
