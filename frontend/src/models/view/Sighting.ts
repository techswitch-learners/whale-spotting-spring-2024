import BodyOfWater from "./BodyOfWater"
import Species from "./Species"
import Reaction from "./Reaction"
import ReactionType from "../../types/ReactionType"
interface Sighting {
  id: number
  latitude: number
  longitude: number
  userName: string
  species: Species
  description: string
  imageUrl: string
  bodyOfWater: BodyOfWater
  sightingTimestamp: string
  reactions: Reaction
  currentUserReaction: ReactionType | null
}

export default Sighting
