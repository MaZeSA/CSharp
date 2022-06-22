const LoginPge = () => {
    return (
        <div className="container">
            <section className="vh-100 bg-image">
                <div className="mask d-flex align-items-center h-100 gradient-custom-3">
                    <div className="container h-100">
                        <div className="row d-flex justify-content-center align-items-center h-100">
                            <div className="col-12 col-md-9 col-lg-7 col-xl-6">
                                <div className="card">
                                    <div className="card-body p-5">
                                        <h2 className="text-uppercase text-center mb-5">Увійти в акаунт</h2>
                                        <form className="was-validated">

                                            <div className="mb-3">
                                                <label className="form-label">Імя користувача:</label>
                                                <input type="text" className="form-control" id="uname" placeholder="Імя користувача" name="uname" required />
                                                <div className="invalid-feedback">Уведійть імя користувача.</div>
                                            </div>
                                            <div className="mb-3">
                                                <label className="form-label">Пароль:</label>
                                                <input type="password" className="form-control" id="pwd" placeholder="Пароль" name="pswd" required />
                                                <div className="invalid-feedback">Уведійть пароль.</div>
                                            </div>
                                            <button type="submit" className="btn btn-primary">Увійти</button>

                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    );
}

export default LoginPge;