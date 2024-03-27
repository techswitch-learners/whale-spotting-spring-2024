import Species from "./Species"

interface ViewingSuggestion {
  id: number
  hotSpotId: number
  hotSpot: null
  speciesId: number
  species: Species
  platforms: string
  platformBoxes: number[]
  timeOfYear: string
  months: number[]
}

interface HotSpot {
  id: number
  name: string
  latitude: number
  longitude: number
  country: string
  viewingSuggestions: ViewingSuggestion[]
}

export default HotSpot
