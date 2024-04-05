import ReactionType from "../../types/ReactionType"

interface AddOrUpdateReactionRequest {
  type: ReactionType
  sightingId: number
}

export default AddOrUpdateReactionRequest
