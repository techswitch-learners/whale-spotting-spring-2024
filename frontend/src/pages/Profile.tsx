import { useContext } from "react"
import { Navigate } from "react-router-dom"
import { AuthContext } from "../App"

const Profile = () => {
  const authContext = useContext(AuthContext)

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  return <h1 className="text-center">Profile</h1>
}

export default Profile
