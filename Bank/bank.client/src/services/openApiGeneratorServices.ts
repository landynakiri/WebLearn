import { Configuration } from '../generated/runtime';
import { WeatherForecastApi, UsersApi } from '../generated/apis';

let config: Configuration;
let weatherForecastApi: WeatherForecastApi;
let userApi: UsersApi = new UsersApi(new Configuration({ basePath: window.location.origin }));

export function getWeatherForecastApi() {
    return weatherForecastApi;
}

export function getUserApi() {
    return userApi;
}

export function initApiConfig() {
    config = new Configuration({basePath: window.location.origin});
    weatherForecastApi = new WeatherForecastApi(config);
    userApi = new UsersApi(config);
}