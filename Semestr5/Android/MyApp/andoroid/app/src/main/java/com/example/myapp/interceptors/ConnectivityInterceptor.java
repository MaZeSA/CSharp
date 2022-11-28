package com.example.myapp.interceptors;


import android.content.Context;

import com.example.myapp.application.HomeApplication;
import com.example.myapp.utils.CommonUtils;
import com.example.myapp.utils.NetworkUtils;

import java.io.IOException;

import okhttp3.Interceptor;
import okhttp3.Request;
import okhttp3.Response;

public class ConnectivityInterceptor implements Interceptor {
    @Override
    public Response intercept(Interceptor.Chain chain) throws IOException {
        Context context= HomeApplication.getAppContext();
        Request originalRequest = chain.request();

        if (!NetworkUtils.isOnline(context)) {
            //MyApplication beginApplication = (MyApplication) context;
            CommonUtils.hideLoading();
            //((ConnectionInternetError) HomeApplication.getCurrentActivity()).navigateErrorPage();
        }
        Request newRequest = originalRequest.newBuilder()
                .build();
        return chain.proceed(newRequest);
    }
}
}
