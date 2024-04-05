import { useContext, useEffect, useState } from "react"
import { Button, Stack } from "react-bootstrap"
import { useNavigate } from "react-router-dom"
import { AuthContext } from "../App"
import { addReaction, updateReaction, deleteReaction } from "../api/backendClient"
import Reactions from "../models/view/Reactions"
import ReactionType from "../types/ReactionType"

const emojis = {
  LetsParty: "🤩",
  NiceCatch: "😊",
  SoSo: "😐",
  Suspicious: "🤔",
}
interface ReactionsCardProps {
  reactions: Reactions
  currentUserReaction: ReactionType | null
  sightingId: number
}

const ReactionsCard = ({ reactions, currentUserReaction, sightingId }: ReactionsCardProps) => {
  const authContext = useContext(AuthContext)
  const navigate = useNavigate()

  const [reactionResponse, setReactionResponse] = useState<ReactionsCardProps>({
    reactions: reactions,
    currentUserReaction: currentUserReaction,
    sightingId: sightingId,
  })
  const [, setErrors] = useState<{ [subject: string]: string[] }>({})

  useEffect(() => {
    console.log("reaction response is changed")
  }, [reactionResponse])

  const deleteUserReaction = () => {
    deleteReaction(sightingId, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setReactionResponse(data))
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to delete your reaction"] }))
  }

  const updateUserReaction = (value: ReactionType) => {
    updateReaction({ reactionType: value, sightingId: sightingId }, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => {
            setReactionResponse(data)
          })
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to update reaction"] }))
  }

  const addUserReaction = (value: ReactionType) => {
    addReaction({ reactionType: value, sightingId: sightingId }, authContext.cookie.token)
      .then((response) => {
        if (response.ok) {
          response.json().then((data) => setReactionResponse(data))
        } else if (response.status === 401) {
          authContext.removeCookie("token")
          navigate("/login")
        } else {
          response.json().then((content) => {
            setErrors(content.errors)
          })
        }
      })
      .catch(() => setErrors({ General: ["Unable to add reaction"] }))
  }

  const handleClick = (value: ReactionType) => {
    if (reactionResponse.currentUserReaction === value) {
      deleteUserReaction()
    } else if (value !== reactionResponse.currentUserReaction && reactionResponse.currentUserReaction !== null) {
      updateUserReaction(value)
    } else {
      addUserReaction(value)
    }
  }

  return (
    <>
      <Stack direction="horizontal">
        {Object.entries(emojis).map(([key, emoji]) => (
          <div key={key}>
            {key === reactionResponse.currentUserReaction && (
              <Button variant={"warning"} onClick={() => handleClick(key as ReactionType)}>
                {emoji}
              </Button>
            )}
            {!(key === reactionResponse.currentUserReaction) && (
              <Button variant="light" onClick={() => handleClick(key as ReactionType)}>
                {emoji}
              </Button>
            )}
            <span style={{ position: "relative", top: "0.75rem" }}>
              {reactionResponse.reactions[key as ReactionType]}
            </span>
          </div>
        ))}
      </Stack>
    </>
  )
}

export default ReactionsCard
