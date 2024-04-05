import ReactionType from "../../types/ReactionType"
import Reaction from "./Reactions"
import Species from "./Species"

interface Sighting {
  id: number
  latitude: number
  longitude: number
  userName: string
  species: Species
  description: string
  imageUrl: string
  bodyOfWater: string
  sightingTimestamp: string
  reactions: Reaction
  currentUserReaction: ReactionType | null
}

export default Sighting
