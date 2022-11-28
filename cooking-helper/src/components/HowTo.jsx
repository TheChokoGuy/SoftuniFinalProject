import React from 'react'

function HowTo({title, image}) {
  return (
    <div className="how-to" style={{backgroundImage: `url(${image})`}}>
        <h1>{title}</h1>
    </div>
  )
}

export default HowTo