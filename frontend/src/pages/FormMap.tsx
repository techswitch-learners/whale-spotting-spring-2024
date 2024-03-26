import { MapContainer, TileLayer, useMapEvents } from "react-leaflet"
import { useState } from "react"
import "leaflet/dist/leaflet.css"
import "./FormMap.scss"

interface GetLocationProps {
  setLatitude: (latitude: number) => void
  setLongitude: (longitude: number) => void
}

function GetLocation({ setLatitude, setLongitude }: GetLocationProps) {
  useMapEvents({
    click: (event) => {
      setLatitude(event.latlng.lat)
      setLongitude(event.latlng.lng)
    },
  })
  return null
}

const SightingForm = () => {
  const [latitude, setLatitude] = useState<number>()
  const [longitude, setLongitude] = useState<number>()

  return (
    <div>
      <form>
        <label>
          {" "}
          Latitude:
          <br />
          <input name="latitude" type="number" value={latitude}></input>
        </label>
        <label>
          {" "}
          Latitude:
          <br />
          <input name="longitude" type="number" value={longitude}></input>
        </label>
      </form>
      <div>
        <MapContainer center={[51.505, -0.09]} zoom={5}>
          <TileLayer
            attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          />
          <GetLocation setLatitude={setLatitude} setLongitude={setLongitude} />
        </MapContainer>
      </div>
    </div>
  )
}

export default SightingForm
