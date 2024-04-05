import { useContext, useState, FormEventHandler } from "react"
import { useNavigate, Link, Navigate } from "react-router-dom"
import { Button, Card, CardText, Form, Spinner } from "react-bootstrap"
import { loginUser } from "../api/backendClient"
import { AuthContext } from "../App"

const Login = () => {
  const navigate = useNavigate()

  const authContext = useContext(AuthContext)

  const [username, setUsername] = useState<string>("")
  const [password, setPassword] = useState<string>("")
  const [loading, setLoading] = useState<boolean>(false)
  const [error, setError] = useState<string>()

  const tryLogin: FormEventHandler = (event) => {
    event.preventDefault()
    setLoading(true)
    setError(undefined)
    loginUser(username, password)
      .then((response) => {
        if (response.ok) {
          response
            .json()
            .then((content: { token: string }) =>
              authContext.setCookie("token", content.token, { secure: true, sameSite: "strict" }),
            )
          navigate("/")
        } else {
          authContext.removeCookie("token")
          setError("Username-password combination not recognised.")
        }
      })
      .catch(() => {
        authContext.removeCookie("token")
        setError("Unable to login at this time. Please try again later.")
      })
      .finally(() => setLoading(false))
  }

  if (authContext.cookie.token) {
    return <Navigate to="/" />
  }

  return (
    <div className="d-flex flex-column justify-content-center flex-grow-1 mw-100 mx-auto" style={{ width: "20rem" }}>
      <Card className="text-center">
        <Card.Header>Welcome back.</Card.Header>
        <Card.Body>
          <Card.Title>Log in</Card.Title>
          <Form onSubmit={tryLogin}>
            <Form.Group className="mb-3 text-start" controlId="username">
              <Form.Label className="mb-1">Username</Form.Label>
              <Form.Control
                type="text"
                value={username}
                onChange={(event) => {
                  setUsername(event.target.value)
                  setError(undefined)
                }}
              />
            </Form.Group>
            <Form.Group className="mb-3 text-start" controlId="password">
              <Form.Label className="mb-1">Password</Form.Label>
              <Form.Control
                type="password"
                value={password}
                onChange={(event) => {
                  setPassword(event.target.value)
                  setError(undefined)
                }}
              />
            </Form.Group>
            <Button variant="primary" type="submit" disabled={loading}>
              {loading ? <Spinner variant="light" size="sm" /> : <>Submit</>}
            </Button>
            {error && <CardText className="text-danger mt-2">{error}</CardText>}
          </Form>
        </Card.Body>
        <Card.Footer className="text-muted">
          Don't have an account yet?
          <br />
          <Link to="/register">Register</Link> instead.
        </Card.Footer>
      </Card>
    </div>
  )
}

export default Login
