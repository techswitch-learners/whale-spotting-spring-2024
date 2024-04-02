import { useContext, useEffect, useState } from "react"
import { Navigate } from "react-router-dom"
import { getUsers } from "../api/backendClient"
import AdminUser from "../models/view/AdminUser"
import { AuthContext } from "../App"

const Users = () => {
  const [allUsers, setAllUsers] = useState<AdminUser[]>()
  const [error, setError] = useState(false)
  const [loading, setLoading] = useState(true)
  const [unauthorisedAccess, setUnauthorisedAccess] = useState(false)

  const authContext = useContext(AuthContext)

  function getData() {
    getUsers(authContext.cookie.token)
      .then((response) => {
        console.log(response.status)
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

  if (!authContext.cookie.token) {
    return <Navigate to="/login" />
  }

  return (
    <div>
      {allUsers && (
        <div>
          <ul>
            {allUsers.map((user) => (
              <li key={user.id}>
                Id: {user.id}, Username: {user.userName}, LockoutEnd: {user.lockOutEnd}, Email: {user.email}
              </li>
            ))}
          </ul>
        </div>
      )}
      {unauthorisedAccess && <p>You shouldn't be here</p>}
      {loading && <p>Loading...</p>}
      {error && <p>Sorry, unable to load user data at this time</p>}
    </div>
  )
}

export default Users
