import { useContext, useEffect, useState } from "react"
import { Button, Stack } from "react-bootstrap"
import { addReaction, updateReaction, deleteReaction } from "../api/backendClient"
import { AuthContext } from "../App"
import { useNavigate } from "react-router-dom"
import ReactionType from "../types/ReactionType"
import Reaction from "../models/view/Reaction"

const emojis = {
  LetsParty: "🤩",
  NiceCatch: "😊",
  SoSo: "😐",
  Suspicious: "🤔",
}
interface ReactionProps {
  reactions: Reaction
  currentUserReaction: ReactionType | null
  sightingId: number
}

const Reactions = ({ reactions, currentUserReaction, sightingId }: ReactionProps) => {
  const authContext = useContext(AuthContext)
  const navigate = useNavigate()

  const [reactionResponse, setReactionResponse] = useState<ReactionProps>({
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
    console.log("update")
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

  const handleClick = async (value: ReactionType) => {
    console.log(`current user reaction: ${currentUserReaction}`)
    console.log(`value: ${value}`)
    if (reactionResponse.currentUserReaction === value) {
      await deleteUserReaction()
      console.log(`---->deleteReactionResponse\n${JSON.stringify(reactionResponse)}`)
    } else if (value !== reactionResponse.currentUserReaction && reactionResponse.currentUserReaction !== null) {
      await updateUserReaction(value)
      console.log(`---->updateReactionResponse\n${JSON.stringify(reactionResponse)}`)
    } else {
      await addUserReaction(value)
      console.log(`---->addReactionResponse\n${JSON.stringify(reactionResponse)}`)
    }
    // console.log(`---->reactionResponse--${JSON.stringify(reactionResponse)}`)
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

export default Reactions
