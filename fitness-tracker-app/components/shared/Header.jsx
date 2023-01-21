import React from 'react'
import Image from 'next/image'
import profilePicture from '../../public/user-profile.png';
import PersonalRecord from './PersonalRecord';

export default function Header() {

    return (
        <div className='container flex items-center justify-between p-3 m-auto'>
            <div className='flex items-center gap-2'>
                <Image src={profilePicture} />
                <p>John Doe</p>
            </div>

            <PersonalRecord />
        </div>
    )
}
