import { Injectable } from '@angular/core';

@Injectable()
export class ConfigurationService {
    get queryServiceUrl(): string {
        return "http://localhost:63432/";
    }

    get commandServiceUrl(): string {
        return "http://localhost:63447/";
    }
}

