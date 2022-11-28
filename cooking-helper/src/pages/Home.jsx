import React from 'react'
import HowTo from '../components/HowTo'
function Home() {
  return (
    <>
    <div className='homepage-title'>
      <h1 className='homepage-title-text'>Easier<br></br>Simpler<br></br>Tastier</h1>
    </div>
    
    <div className='HowTo'>
      <h1 className='how-to-title'>But how?</h1>
    <HowTo title="Select a course"/>
    <HowTo title="Chose a lesson"/>
    <HowTo title="Follow the steps"/>
    <HowTo title="Enjoy a tasty meal"/>

    </div>
    </>
  )
}

export default Home