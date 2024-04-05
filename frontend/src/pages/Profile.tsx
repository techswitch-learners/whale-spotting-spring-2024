import { useContext, useEffect, useState } from "react"
import { Link, Navigate } from "react-router-dom"
import { AuthContext, BackgroundContext } from "../App"
import { getSightings, getUser } from "../api/backendClient"
import Sighting from "../models/view/Sighting"
import Card from "react-bootstrap/esm/Card"
// import calf from "../../public/achievement-badges-images/calf.png"
// import { Image } from "react-bootstrap"

interface SightingCardProps {
  imageUrl: string
  species: string
  bodyOfWater: string
  description: string
  sightingTimestamp: string
}

function SightingCard({ imageUrl, species, bodyOfWater, description, sightingTimestamp }: SightingCardProps) {
  return (
    <Card className="text-start">
      <Card.Img variant="top" src={imageUrl} />
      <Card.Body>
        <Card.Title className="text-center">{species}</Card.Title>
        <Card.Subtitle className="text-center">{bodyOfWater}</Card.Subtitle>
        <Card.Text className="text-center">{description}</Card.Text>
      </Card.Body>
      <Card.Footer className="text-center">
        <small>{sightingTimestamp}</small>
      </Card.Footer>
    </Card>
  )
}

const Profile = () => {
  const authContext = useContext(AuthContext)
  const [userName, setUserName] = useState("")
  const [userId, setUserId] = useState()
  // const [experiencePoints,setExperiencePoints]=useState()
  // const [achievementName, setAchievementName]=useState("")
  // const [profilePic, setProfilePic]=useState("")

  const [allSightings, setAllSightings] = useState<Sighting[]>()
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState(false)

  const backgroundContext = useContext(BackgroundContext)

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  function getUserProfile() {
    setLoading(true)
    setError(false)
    getUser(authContext.cookie.token) // more info to get from this endpoint (achievements) what info we 'd like to have in the profile page
      .then((response) => response.json())
      .then((data) => {
        setUserName(data.userName)
        setUserId(data.id)
        // setExperiencePoints(data.experience);
        // setAchievementName(data.userAchievement.name);
        // setProfilePic(data.userAchievement.badgeImageUrl);
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
    console.log(setUserName)
  }
  function getUserSightings() {
    setLoading(true)
    setError(false)
    getSightings()
      .then((response) => response.json())
      .then((data) => setAllSightings(data.sightings))
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }
  useEffect(getUserProfile, [authContext.cookie.token])
  useEffect(getUserSightings, [])

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  return (
    <div className="Profile d-flex flex-column align-items-center m-4">
      <h1 className="text-center">{userName}'s Profile</h1>
      {/* <Image style={{height:"100px", width:"100px"}} src={`${import.meta.env.BASE_URL}achievement-badges-images/${profilePic}`} alt={`Badge image of `} roundedCircle /> */}
      <h4 className="mt-4 ">UserId: {userId}</h4>
      <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
        {/* <p style={{color:'black',fontSize: '20px' }}>Experience Points: {experiencePoints}</p> */}
        {/* <p style={{color:'black',fontSize: '20px' }}>  Achievement Name: {achievementName} </p>  */}
      </div>
      <p>Please find below Approved sightings</p>
      <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
        {allSightings?.map((sighting) => (
          <Link
            to={`/sightings/${sighting.id}`}
            key={sighting.id}
            className="text-decoration-none"
            style={{ width: "13rem" }}
          >
            <SightingCard
              imageUrl={sighting.imageUrl}
              species={sighting.species.name}
              bodyOfWater={sighting.bodyOfWater}
              description={sighting.description}
              sightingTimestamp={"Observed on " + sighting.sightingTimestamp.split("T")[0]}
            />
          </Link>
        ))}
      </div>
      <p>Please find below Rejected sightings</p>
      <div className="d-flex flex-wrap justify-content-center gap-4 my-4">
        {allSightings?.map((sighting) => (
          <Link
            to={`/sightings/${sighting.id}`}
            key={sighting.id}
            className="text-decoration-none"
            style={{ width: "13rem" }}
          >
            <SightingCard
              imageUrl={sighting.imageUrl}
              species={sighting.species.name}
              bodyOfWater={sighting.bodyOfWater}
              description={sighting.description}
              sightingTimestamp={"Observed on " + sighting.sightingTimestamp.split("T")[0]}
            />
          </Link>
        ))}
      </div>
      {loading && <p>Loading...</p>}
      {error && <p>Error fetching sighting from the backend</p>}
    </div>
  )
}

export default Profile
