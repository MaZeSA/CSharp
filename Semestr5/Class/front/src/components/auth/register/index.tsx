const RegisterPge = () => {
    return (
        <section className="vh-100 bg-image">
            <div className="mask d-flex align-items-center h-100 gradient-custom-3">
                <div className="container h-100">
                    <div className="row d-flex justify-content-center align-items-center h-100">
                        <div className="col-12 col-md-9 col-lg-7 col-xl-6">
                            <div className="card">
                                <div className="card-body p-5">
                                    <h2 className="text-uppercase text-center mb-5">Створити новий акаунт</h2>
                                    <form className="was-validated">

                                        <div className="form-outline mb-4">
                                            <label className="form-label">Імя</label>
                                            <input type="text" id="form3Example1cg" className="form-control form-control-lg" required />
                                        </div>

                                        <div className="form-outline mb-4">
                                            <label className="form-label">Email</label>
                                            <input type="email" id="form3Example3cg" className="form-control form-control-lg" required />
                                        </div>

                                        <div className="form-outline mb-4">
                                            <label className="form-label">Пароль</label>
                                            <input type="password" id="form3Example4cg" className="form-control form-control-lg" required />
                                        </div>

                                        <div className="form-outline mb-4">
                                            <label className="form-label">Повторіть пароль</label>
                                            <input type="password" id="form3Example4cdg" className="form-control form-control-lg" required />
                                        </div>

                                        <div className="d-flex justify-content-center">
                                            <button type="button"
                                                className="btn btn-success btn-block btn-lg gradient-custom-4 text-body">Реєстрація</button>
                                        </div>

                                    </form>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    );
}

export default RegisterPge;