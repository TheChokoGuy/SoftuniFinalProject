import React from 'react'

function Lesson({text, imagePath, className, path}) {
  return (
    <div>
      <a className="tab-link" href={`${path}`}>
        <div className={`lesson`} style={{backgroundImage: `url(${imagePath})`}}>
            <h2 className="lesson-text">{text}</h2>
        </div>
      </a>
    </div>
  )
}

export default Lesson