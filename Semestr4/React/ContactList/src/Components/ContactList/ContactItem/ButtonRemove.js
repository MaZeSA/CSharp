function RemoveButton(id) {
    function handleClick(e) {
      e.preventDefault();
      console.log('Посилання було натиснуте.'); 
      console.log(id);
    }
  
    return (
        <button type="button" onClick={handleClick} className="btn btn-warning btn-xs">
         <i className=""></i> X
       </button>
    );
  }

  export default RemoveButton;