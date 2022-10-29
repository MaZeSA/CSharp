package com.example.myapp.account.network;

import com.example.myapp.account.dto.AccountResponseDTO;
import com.example.myapp.account.dto.LoginDTO;
import com.example.myapp.account.dto.RegisterDTO;
import com.example.myapp.account.dto.UserDTO;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Path;

public interface AccountApi {
        @POST("/api/account/register")
        public Call<AccountResponseDTO> register(@Body RegisterDTO model);
        @POST("/api/account/login")
        public Call<AccountResponseDTO> login(@Body LoginDTO model);
        @GET("/api/account/users")
        public Call<List<UserDTO>> users();

        @GET("/api/account/getusers/{id}")
        public Call<List<UserDTO>> users(@Path("id") int id);
    }
