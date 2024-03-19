import "../main.scss"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import "leaflet/dist/leaflet.css"
import { Icon } from "leaflet"

const Map = () => {
  const sightings = [
    {
      id: 1,
      latitude: -37.439974,
      longitude: 156.445313,
      description: "whale spotting 1",
    },
    {
      id: 2,
      latitude: -49.382372,
      longitude: -79.453125,
      description: "whale spotting 2",
    },
    {
      id: 3,
      latitude: 58.859224,
      longitude: -0.878906,
      description: "whale spotting 3",
    },
  ]

  const customIcon = new Icon({
    iconUrl: "https://cdn-icons-png.flaticon.com/128/8153/8153004.png",
    iconSize: [25, 25],
  })

  return (
    <div>
      <h1 className="text-center">Map</h1>
      <p>Use the zoom button to see the exact location of the whale sighting</p>
      <p>Click on the icon to see more details about the whale sighting</p>
      <MapContainer center={[26.115, -16.523]} zoom={1} scrollWheelZoom={false}>
        <TileLayer
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
        />
        {sightings.map((sighting) => (
          <Marker
            key={sighting.id}
            position={[Number(sighting.latitude), Number(sighting.longitude)]}
            icon={customIcon}
          >
            <Popup>{sighting.description}</Popup>
          </Marker>
        ))}
      </MapContainer>
    </div>
  )
}

export default Map
