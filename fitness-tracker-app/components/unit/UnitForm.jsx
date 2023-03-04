import React from 'react'

export default function UnitForm() {

  return (
    <div className="container flex p-3 m-auto">
      <div className="flex-col basis-full">
        <div className="flex-row justify-items-center">
          <label className="basis-1/4" ><b>Unit</b></label>
          <div classname="flex-row bg-black"> <span></span>

            <div className="flex-row"></div>
            <div className="flex-row"></div>
            <h2 className=" text-black pt-6"><b>Add unit</b></h2>
            <label className=" text-black text-xs">Name</label>
            <br></br>
            <input type="text" name="name" className="basis-3/4 bg-custom-grey  border-custom-white border-solid border rounded h-21 pl-7  "></input>
            <br></br>
            <label className=" text-black text-xs">Description</label>
            <br></br>
            <textarea name="description" className="basis-3/4 bg-custom-grey border-custom-white border-solid border rounded h-32 w-300 pl-7 "></textarea>
          </div>
          <br></br>
          <button type="button" class="bg-custom-blue rounded-2xl text-custom-grey text-xs
           h-8 w-32 mt-4" >Save changes</button>

        </div>
      </div>
    </div>
  )
}
