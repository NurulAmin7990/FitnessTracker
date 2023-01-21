import React, { useState } from 'react'
import FireIcon from '../icons/FireIcon'

export default function PersonalRecord() {
    const [showToolTip, setShowToolTip] = useState(false)

    return (
        <div className='relative group'>
            <div className='flex items-center px-3 py-2 rounded-full cursor-pointer bg-border-color' onClick={() => setShowToolTip(!showToolTip)}>
                <FireIcon />
                <h4 className='font-bold'>14KG</h4>
            </div>

            <p className={`absolute right-0 w-56 px-2 py-4 transition-all border rounded-md pointer-events-none border-border-color ${showToolTip ? "top-14 opacity-100" : "opacity-0 top-10"}`}>
                Loem ipsum dolor sit amet consectetur. Morbi blanditr at tincidunt risus.
            </p>
        </div>
    )
}