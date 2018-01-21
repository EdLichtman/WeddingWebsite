import { Injectable } from "@angular/core";
import * as settings from '../../../config.json'

@Injectable()
export class ConfigurationService {
    apiBaseUri: string = settings["apiBaseUri"];
}