const Header = () => {
  return (
    <div className="container">
      <div className="row">
        <dic className="col-xs-12">
          <nav class="navbar navbar-default navbar-inverse">
            <div class="container-fluid">
              <div class="navbar-header">
                <button
                  type="button"
                  class="navbar-toggle collapsed"
                  data-toggle="collapse"
                  data-target="#bs-example-navbar-collapse-1"
                  aria-expanded="false"
                >
                  <span class="sr-only">Toggle navigation</span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="#">
                  Contact List
                </a>
              </div>

              <div
                class="collapse navbar-collapse"
                id="bs-example-navbar-collapse-1"
              >
                <ul class="nav navbar-nav">
                  <li class="active">
                    <a href="#">
                      Home <span class="sr-only">(current)</span>
                    </a>
                  </li>
                  <li>
                    <a href="#">New contact</a>
                  </li>
                </ul>
                <form class="navbar-form navbar-right">
                  <div class="form-group">
                    <input
                      type="text"
                      class="form-control"
                      placeholder="Search"
                    />
                  </div>
                  <button type="submit" class="btn btn-default">
                    Submit
                  </button>
                </form>
              </div>
            </div>
          </nav>
        </dic>
      </div>
    </div>
  );
};

export default Header;
