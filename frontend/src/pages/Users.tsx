import { useContext, useEffect, useState } from "react"
import { Button, Card, Spinner } from "react-bootstrap"
import { Navigate, useNavigate } from "react-router-dom"
import { AuthContext, BackgroundContext } from "../App"
import { getUsers } from "../api/backendClient"
import { deleteUser, resetProfilePicture } from "../api/backendClient"
import DetailedUser from "../models/view/DetailedUser"
import Error403 from "./Error403"
import whale from "/android-chrome-512x512.png"

interface UserCardProps {
  userName: string
  profileImageUrl: string
  handleProfilePicture: (userName: string) => void
  handleDeleteUser: (userName: string) => void
}

function UserCard({ userName, profileImageUrl, handleDeleteUser, handleProfilePicture }: UserCardProps) {
  return (
    <Card className="text-start" style={{ width: "15rem" }}>
      <Card.Img
        className="p-2 bg-dark"
        variant="top"
        src={profileImageUrl || whale}
        style={{ height: "15rem", objectFit: "contain" }}
        alt="user profile picture"
      />
      <Card.Body>
        <Card.Title>{userName}</Card.Title>
      </Card.Body>
      <Card.Footer className="text-center">
        <Button className="me-2" onClick={() => handleProfilePicture(userName)}>
          Reset picture
        </Button>
        <Button variant="danger" onClick={() => handleDeleteUser(userName)}>
          Delete
        </Button>
      </Card.Footer>
    </Card>
  )
}

const Users = () => {
  const backgroundContext = useContext(BackgroundContext)
  const [allUsers, setAllUsers] = useState<DetailedUser[]>()
  const [error, setError] = useState(false)
  const [loading, setLoading] = useState(true)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)

  const authContext = useContext(AuthContext)
  const navigate = useNavigate()

  function getData() {
    setLoading(true)
    setError(false)
    getUsers(authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setAllUsers(data))
        } else if (response.status === 403) {
          setUnauthorisedAccess(true)
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        }
      })
      .catch(() => setError(true))
      .finally(() => setLoading(false))
  }

  useEffect(getData, [authContext, navigate])

  useEffect(() => {
    if (!unauthorisedAccess) {
      backgroundContext.setBackground("white")
    }
  }, [backgroundContext, unauthorisedAccess])

  function handleProfilePicture(userName: string) {
    resetProfilePicture(userName, authContext.cookie.token).then((response) => {
      if (response.ok) {
        getData()
      } else if (response.status === 403) {
        setUnauthorisedAccess(true)
      } else if (response.status === 401) {
        authContext.removeCookie("token")
        navigate("/login")
      }
    })
  }

  function handleDeleteUser(userName: string) {
    deleteUser(userName, authContext.cookie.token).then((response) => {
      if (response.ok) {
        setAllUsers(allUsers?.filter((user) => user.userName != userName))
      } else if (response.status === 403) {
        setUnauthorisedAccess(true)
      } else if (response.status === 401) {
        authContext.removeCookie("token")
        navigate("/login")
      }
    })
  }

  if (unauthorisedAccess) {
    return <Error403 />
  }

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  return (
    <div className="mb-3">
      {allUsers && (
        <>
          <h2 className="text-center">Total users: {allUsers.length}</h2>
          <div className="d-flex flex-wrap justify-content-center gap-4 pb-3">
            {allUsers.map((user) => (
              <UserCard
                key={user.id}
                userName={user.userName}
                profileImageUrl={user.profileImageUrl}
                handleDeleteUser={handleDeleteUser}
                handleProfilePicture={handleProfilePicture}
              />
            ))}
          </div>
        </>
      )}
      {loading && (
        <p>
          Loading...
          <Spinner />
        </p>
      )}
      {error && <p>Couldn't load data at this time</p>}
    </div>
  )
}

export default Users
