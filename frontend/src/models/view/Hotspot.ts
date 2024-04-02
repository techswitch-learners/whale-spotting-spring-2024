import Species from "./Species"

interface ViewingSuggestion {
  id: number
  HotspotId: number
  speciesId: number
  species: Species
  platforms: string
  platformBoxes: number[]
  timeOfYear: string
  months: number[]
}

interface Hotspot {
  id: number
  name: string
  latitude: number
  longitude: number
  country: string
  viewingSuggestions: ViewingSuggestion[]
}

export default Hotspot
