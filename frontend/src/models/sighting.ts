interface Sighting {
  latitude?: number
  longitude?: number
  token?: string
  speciesId?: number
  description?: string
  imageUrl?: string
  bodyOfWaterId?: number
  sightingTimeStamp?: Date
}

export default Sighting
