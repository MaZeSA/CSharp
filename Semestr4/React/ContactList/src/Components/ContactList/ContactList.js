// import components
import React from "react";
import ContactItem from "./ContactItem/ContactItem";

class ContactList extends React.Component {
  constructor(props) {
    console.log(props);
    super(props);
    this.state = { contactList: props.List };
    this.removeClick = this.removeClick.bind(this);
    this.ratingChange = this.ratingChange.bind(this);
  }
  contact() {
    return this.state.contactList.map((item) => {
      item.eventRemove = this.removeClick;
      item.ratingChange = this.ratingChange;
      return <ContactItem key={item.Id} {...item} />;
    });
  }

  removeClick(id) {
    const mas = this.state.contactList.filter((item) => item.Id != id);
    this.setState(() => ({
      contactList: mas
    }));
  }
  ratingChange(id, newRating) {
    this.state.contactList.forEach(element => {
      if (element.Id == id) {
        element.Rating = newRating;
      }
    });
  }

  render() {
    return (
      <div className="container">
        <div className="col-md-12 bootstrap snippets bootdeys">
          <div className="x_panel">
            <div className="x_content">
              <div className="row">{this.contact()}</div>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default ContactList;
