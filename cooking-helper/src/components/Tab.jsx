import * as React from 'react'
import {Link} from 'react-router-dom'
function Tab({text, imagePath, className, path}) {
  return (
    <div>
      <Link to={path} className='tab-link'>
        <div className={`tab ${className}`} style={{backgroundImage: `url(${imagePath})`}}>
            <h2 className="text"  style={{ flex: 1, justifyContent: 'center', alignItems:"center", lineHeight:"100px"}}>{text}</h2>
        </div>
      </Link>
    </div>
  )
}

export default Tab