import React from 'react'
import DeleteIcon from '../icons/DeleteIcon'

export default function Table() {

    const headStyling = "table-cell font-bold px-3 py-2"
    const description = "This is the CM dskfa kdjfk jsdlkfjdsf"

    const units = [
        {
            'id': 1,
            'unit': 'KM',
            'description': 'The distance is measured in KM'
        },
        {
            'id': 2,
            'unit': 'LBS',
            'description': 'This is kinda weird, becuase the pounds'
        },
        {
            'id': 3,
            'unit': 'KG',
            'description': 'This is the Kilograms unit'
        },
    ]

    return (
        <div className="container p-3">
            <div className='table w-full m-auto border rounded-md border-gray-divider'>
                <div className='table-header-group'>
                    <div className="relative table-row bg-gray-100">
                        <p className={headStyling}>Unit</p>
                        <p className={headStyling}>Description</p>
                        <button onClick={() => console.log('button click')} className='absolute top-0 right-0 w-32 py-2 text-white bg-blue-500 rounded-tr-md'>Add unit</button>
                    </div>
                </div>
                <div className='table-row-group divide-y border-gray-divider'>
                    {units.map(unit => (
                        <div key={unit.id} className="table-row">
                            <p className='table-cell px-3 py-2'>{unit.unit}</p>
                            <p className='table-cell px-3 py-2'>{unit.description.substring(0, 25)}...</p>
                            <button onClick={() => console.log(unit.id)} className='pt-4'><DeleteIcon /></button>
                        </div>
                    ))}
                </div>
            </div>

        </div>
    )
}

// 