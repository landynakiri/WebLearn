import { Configuration } from '../generated/runtime';
import { WeatherForecastApi, UsersApi } from '../generated/apis';

const config = new Configuration({ basePath: window.location.origin });

export const weatherForecastApi = new WeatherForecastApi(config)
export const userApi = new UsersApi(config)