import { useContext, useState, FormEventHandler, Fragment } from "react"
import { Link, Navigate } from "react-router-dom"
import { Button, Card, CardText, Form, Spinner } from "react-bootstrap"
import { registerUser } from "../api/backendClient"
import { AuthContext } from "../App"

const Register = () => {
  const authContext = useContext(AuthContext)

  const [username, setUsername] = useState<string>("")
  const [password, setPassword] = useState<string>("")
  const [loading, setLoading] = useState<boolean>(false)
  const [success, setSuccess] = useState<boolean>(false)
  const [errors, setErrors] = useState<string[]>([])

  const tryRegister: FormEventHandler = (event) => {
    event.preventDefault()
    setLoading(true)
    setSuccess(false)
    setErrors([])
    registerUser(username, password)
      .then((response) => {
        if (response.ok) {
          setUsername("")
          setPassword("")
          setSuccess(true)
        } else {
          response.json().then((content: { errors: string[] }) => setErrors(content.errors))
        }
      })
      .catch(() => setErrors(["Unable to complete registration at this time. Please try again later."]))
      .finally(() => setLoading(false))
  }

  if (authContext.cookie.token) {
    return <Navigate to="/" />
  }

  return (
    <div className="d-flex flex-column justify-content-center mx-auto" style={{ maxWidth: "20rem", minHeight: "100%" }}>
      <Card className="text-center">
        <Card.Header>Welcome.</Card.Header>
        <Card.Body>
          <Card.Title>Register</Card.Title>
          <Form onSubmit={tryRegister}>
            <Form.Group className="mb-3 text-start" controlId="username">
              <Form.Label className="mb-1">Username</Form.Label>
              <Form.Control
                type="text"
                value={username}
                onChange={(event) => {
                  setUsername(event.target.value)
                  setSuccess(false)
                  setErrors([])
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
                  setSuccess(false)
                  setErrors([])
                }}
              />
            </Form.Group>
            <Button variant="primary" type="submit" disabled={loading}>
              {loading ? <Spinner variant="light" size="sm" /> : <>Submit</>}
            </Button>
            {success && (
              <CardText className="text-success mt-2 mb-0">
                Registration complete!
                <br />
                You can now{" "}
                <Link to="/login" className="link-success">
                  log in
                </Link>
                .
              </CardText>
            )}
            {errors.length > 0 && (
              <>
                <CardText className="text-danger mt-2 mb-0">
                  Error{errors.length > 1 ? "s" : ""} encountered during registration:
                </CardText>
                <ul className="list-unstyled mb-0 small">
                  {errors.map((error) => (
                    <Fragment key={error}>
                      <li className="d-inline text-danger">{error}</li>{" "}
                    </Fragment>
                  ))}
                </ul>
              </>
            )}
          </Form>
        </Card.Body>
        <Card.Footer className="text-muted">
          Already have an account?
          <br />
          <Link to="/login">Log in</Link> instead.
        </Card.Footer>
      </Card>
    </div>
  )
}

export default Register
