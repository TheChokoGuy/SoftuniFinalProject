import React from 'react'
function NavLinks() {
  return (
    <ul>
        <li className='navlinks-li'>
            <a className='navlinks-li-a' href="/">Home</a>
        </li>
        <li>
            <a className='navlinks-li-a' href="/courses">Courses</a>
        </li>
        <li>
            <a className='navlinks-li-a' href="/">Tools</a>
        </li>
    </ul>
  )
}

export default NavLinks