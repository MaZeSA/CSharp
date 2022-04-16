import StarComponent from "./StarComponent"
import { v4 } from "uuid";
import React, { Fragment } from "react";

class ContactItem extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      rating: props.Rating
    };
    this.Props = props;
    this.URL = `https://randomuser.me/api/portraits/${this.Props.Gender}/${this.Props.Avatar}.jpg`;
    this.eventRating = this.eventRating.bind(this);
  }

  eventRating = (newRating) => {
    this.Props.ratingChange(this.Props.Id, newRating);
  };

  render() {
    return (
      <div className="col-md-4 col-sm-4 col-xs-12 animated fadeInDown" >
        <div className="well profile_view">
          <div className="col-sm-12">
            <h4 className="brief">
              <i>{this.Props.Position}</i>
            </h4>
            <div className="left col-xs-7">
              <h2>{this.Props.Name}</h2>
              <p>
                <strong>About: </strong> {this.Props.About}{" "}
              </p>
              <ul className="list-unstyled">
                <li>
                  <i className="fa fa-phone"></i> Phone: {this.Props.Phone}{" "}
                </li>
              </ul>
            </div>
            <div className="right col-xs-5 text-center">
              <img src={this.URL} alt="" className="img-circle img-responsive" />
            </div>
          </div>
          <div className="col-xs-12 bottom text-center">
            <div className="col-xs-12 col-sm-6 emphasis">
              <StarComponent  Count={5} Rating={this.state.rating} Event={this.eventRating} />
            </div>
            <div className="col-xs-12 col-sm-6 emphasis">
              <button type="button" className="btn btn-success btn-xs">
                <i className="fa fa-user"></i>
                <i className="fa fa-comments-o"></i>
              </button>
              <button type="button" className="btn btn-primary btn-xs">
                <i className="fa fa-user"></i> View Profile
              </button>
              <button type="button" onClick={() => this.Props.eventRemove(this.Props.Id)} className="btn btn-warning btn-xs">
                <i className=""></i> X
              </button>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default ContactItem;
