import ReactionType from "../../types/types"

interface ReactionRequest {
  reactionType: ReactionType
  sightingId: number
}

export default ReactionRequest
