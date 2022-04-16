import ReactDOM from "react-dom";
import React, { Fragment } from "react";
import "./index.css";
import { v4 as uuidv4 } from "uuid";

// Import components
import Header from "./Components/Header/Hader";
import ContactList from "./Components/ContactList/ContactList";
import { v4 } from "uuid";

class App1 extends React.Component {
  constructor(props) {
    super(props);
    this.state = {ContactList: this.ContactListArray};
  }
  ContactListArray = [
    {
      Id: v4(),
      Position: ".NET Developer",
      Name: "Alvaro Moreno",
      About: "About Alvaro",
      Phone: "+380(97-457-52-13)",
      Gender: "men",
      Avatar: 11,
      Rating: 2,
    },
    {
      Id: v4(),
      Position: "Web UI Designer",
      Name: "Camilla Anderson",
      About: "About Camilla",
      Phone: "+380(95-781-29-84)",
      Gender: "women",
      Avatar: 11,
      Rating: 4.0,
    },
    {
      Id: v4(),
      Position: "QA Engeneer",
      Name: "Izabella Fox",
      About: "Best QA and manual testing",
      Phone: "+380(95-781-29-51)",
      Gender: "women",
      Avatar: 25,
      Rating: 3,
    },
    {
      Id: v4(),
      Position: "Team Lead",
      Name: "Bob Robinson",
      About: ".Net team lead",
      Phone: "+380(95-781-02-51)",
      Gender: "men",
      Avatar: 25,
      Rating: 5,
    },
  ];
  render() {
    return (
      <Fragment>
      <Header />
      <ContactList List={this.state.ContactList} />
    </Fragment>
    );
  }
}

ReactDOM.render(<App1 />, document.getElementById("root"));
