import { useContext, useEffect, useState } from "react"
import { getUsers } from "../api/backendClient"
import DetailedUser from "../models/view/DetailedUser"
import { AuthContext, BackgroundContext } from "../App"
import { deleteUser, editProfilePicture } from "../api/backendClient"
import { Button, Card } from "react-bootstrap"
import whale from "/android-chrome-512x512.png"
import Error403 from "./Error403"

const Users = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [allUsers, setAllUsers] = useState<DetailedUser[]>()
  const [error, setError] = useState(false)
  const [loading, setLoading] = useState(true)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)

  const authContext = useContext(AuthContext)

  function getData() {
    getUsers(authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setAllUsers(data))
        } else if (response.status === 403 || response.status === 401) {
          authContext.removeCookie("token")
          setUnauthorisedAccess(true)
        }
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  useEffect(getData, [authContext])

  useEffect(() => {
    backgroundContext.setBackground("white")
  }, [backgroundContext])

  function handleProfilePicture(userName: string, authContext: string | undefined) {
    editProfilePicture(userName, authContext, whale).then((response) => {
      if (response.ok) {
        getData()
      }
    })
  }

  function handleDeleteUser(userName: string, authContext: string | undefined) {
    deleteUser(userName, authContext).then((response) => {
      if (response.ok) {
        setAllUsers(allUsers?.filter((user) => user.userName != userName))
      }
    })
  }

  interface UserCardProps {
    userName: string
    profileImageUrl: string
  }

  function UserCard({ userName, profileImageUrl }: UserCardProps) {
    return (
      <Card className="text-start">
        {profileImageUrl && (
          <Card.Img
            variant="top"
            src={profileImageUrl}
            style={{ height: "13rem", width: "auto" }}
            alt="user's profile picture"
          />
        )}
        <Card.Body>
          <Card.Title>{userName}</Card.Title>
        </Card.Body>
        <Card.Footer>
          <Button className="mx-2" onClick={() => handleProfilePicture(userName, authContext.cookie.token)}>
            {" "}
            Reset Profile Picture
          </Button>
          <Button
            className="mx-2"
            variant="danger"
            onClick={() => handleDeleteUser(userName, authContext.cookie.token)}
          >
            Delete
          </Button>
        </Card.Footer>
      </Card>
    )
  }

  return (
    <div className="mb-3">
      {allUsers && (
        <>
          <h2 className="text-center">Total users: {allUsers.length}</h2>
          <div className="d-flex flex-wrap justify-content-center gap-4 pb-3">
            {allUsers.map((user) => (
              <UserCard key={user.id} userName={user.userName} profileImageUrl={user.profileImageUrl} />
            ))}
          </div>
        </>
      )}
      {unauthorisedAccess && <Error403 />}
      {loading && <p>Loading...</p>}
      {error && <p>Sorry, unable to load user data at this time</p>}
    </div>
  )
}

export default Users
