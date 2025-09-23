import { Configuration } from '../generated/runtime';
import { WeatherForecastApi } from '../generated/apis/WeatherForecastApi';

const config = new Configuration({ basePath: window.location.origin });

export const weatherForecastApi = new WeatherForecastApi(config)