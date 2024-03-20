import "./Map.scss"
import { useState } from "react"
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet"
import "leaflet/dist/leaflet.css"
import { Icon } from "leaflet"
import MarkerClusterGroup from "react-leaflet-cluster"
import Form from "react-bootstrap/Form"

interface SightingsCardProps {
  imgUrl: string
  species: string
  bodyOfWater: string
  description: string
}

const SightingsCard = ({ imgUrl, species, bodyOfWater, description }: SightingsCardProps) => {
  return (
    <div className="sightingsCard">
      <img src={imgUrl} />
      <p>Species: {species}</p>
      <p>Body of Water: {bodyOfWater}</p>
      <p>{description}</p>
    </div>
  )
}

const Map = () => {
  const [mapView, setMapView] = useState<boolean>(false)

  //     interface SightingsData{
  //     id: number,
  //     latitude: number,
  //     longitude: number,
  //     species: {
  //       name: string
  //     },
  //     description: string,
  //     bodyOfWater: string,
  //     imageUrl: string,
  //   }

  //   const [allSightings, setAllSightings]= useState<SightingsData>();
  //   const [error, setErrror] = useState(false);

  // useEffect(()=>{
  //   fetch("htpps://localhost:5280/all")
  //   .then(response=>response.json())
  //   .then(data=>setAllSightings(data))
  //   .catch(() => setErrror(true))
  // },[])

  const sightings = [
    {
      id: 1,
      latitude: -37.439974,
      longitude: 156.445313,
      species: "blue whale",
      bodyOfWater: "Tasman Sea",
      imageUrl: "https://picsum.photos/200/300?grayscale",
      description: "whale spotting 1",
    },
    {
      id: 2,
      latitude: -49.382372,
      longitude: -79.453125,
      species: "orca",
      bodyOfWater: "South Pacific Ocean",
      imageUrl: "https://picsum.photos/200/300?grayscale",
      description: "whale spotting 2",
    },
    {
      id: 3,
      latitude: 58.859224,
      longitude: -0.878906,
      species: "grey whale",
      bodyOfWater: "North Sea",
      imageUrl: "https://picsum.photos/200/300?grayscale",
      description: "whale spotting 3",
    },
    {
      id: 4,
      latitude: 59.859224,
      longitude: -0.878906,
      species: "toothed whale",
      bodyOfWater: "North Sea",
      imageUrl: "https://picsum.photos/200/300?grayscale",
      description: "whale spotting 4",
    },
    {
      id: 5,
      latitude: 59.859224,
      longitude: -0.877906,
      species: "toothed whale",
      bodyOfWater: "North Sea",
      imageUrl: "https://picsum.photos/200/300?grayscale",
      description: "whale spotting 5",
    },
  ]

  const customIcon = new Icon({
    iconUrl: "https://cdn-icons-png.flaticon.com/128/8153/8153004.png",
    iconSize: [25, 25],
  })

  const toggleView = () => {
    mapView ? setMapView(false) : setMapView(true)
  }

  return (
    <div className="mainContainer">
      <h1>Whale Sightings</h1>
      <Form>
        <Form.Check type="switch" id="custom-switch" label={mapView ? "Map View" : "List View"} onClick={toggleView} />
      </Form>
      {mapView && (
        <div>
          <p>
            Use the zoom button to see the exact location of the whale sighting <br /> Click on the icon to see more
            details about the whale sighting
          </p>
          <p></p>
          <MapContainer center={[26.115, -16.523]} zoom={1} scrollWheelZoom={false}>
            <TileLayer
              attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
              url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            />
            <MarkerClusterGroup>
              {sightings.map((sighting) => (
                <Marker
                  key={sighting.id}
                  position={[Number(sighting.latitude), Number(sighting.longitude)]}
                  icon={customIcon}
                >
                  <Popup>{sighting.description}</Popup>
                </Marker>
              ))}
            </MarkerClusterGroup>
          </MapContainer>
        </div>
      )}
      {!mapView && (
        <div className="sightingsContainer">
          {sightings.map((sighting) => (
            <SightingsCard
              key={sighting.id}
              imgUrl={sighting.imageUrl}
              species={sighting.species}
              bodyOfWater={sighting.bodyOfWater}
              description={sighting.description}
            />
          ))}
        </div>
      )}
    </div>
  )
}

export default Map
