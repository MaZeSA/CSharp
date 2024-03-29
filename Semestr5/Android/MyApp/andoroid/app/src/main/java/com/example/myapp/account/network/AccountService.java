package com.example.myapp.account.network;

import com.example.myapp.constans.Urls;
import com.example.myapp.network.interceptors.JWTInterceptor;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class AccountService {
    private static AccountService instance;
    private Retrofit retrofit;

    private AccountService() {
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .connectTimeout(20, TimeUnit.SECONDS)
                .writeTimeout(20, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .addInterceptor(new JWTInterceptor())
                .build();
        retrofit = new Retrofit.Builder()
                .client(okHttpClient)
                .baseUrl(Urls.BASE)
                .addConverterFactory(GsonConverterFactory.create())
                .build();
    }

    public static AccountService getInstance() {
        if(instance==null)
            instance=new AccountService();
        return instance;
    }

    public AccountApi jsonApi() {
        return retrofit.create(AccountApi.class);
    }
}
