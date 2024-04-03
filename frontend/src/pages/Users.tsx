import { useContext, useEffect, useState } from "react"
import { getUsers } from "../api/backendClient"
import AdminUser from "../models/view/DetailedUser"
import { AuthContext } from "../App"
import { deleteUser } from "../api/backendClient"

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

  // if (!authContext.cookie.token) {
  //   return <Navigate to="/login" />
  // }

  function handleClick(id: number, authContext: string | undefined) {
    deleteUser(id, authContext).then((response) => {
      if (response.ok) {
        setAllUsers(allUsers?.filter((user) => user.id != id))
      }
    })
  }

  return (
    <div>
      {allUsers && (
        <div>
          <ul>
            {allUsers.map((user) => (
              <li key={user.id}>
                Id: {user.id}, Username: {user.userName}, Phone: {user.phoneNumber}, Email: {user.email}
                <button onClick={() => handleClick(user.id, authContext.cookie.token)}> Delete user</button>
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
