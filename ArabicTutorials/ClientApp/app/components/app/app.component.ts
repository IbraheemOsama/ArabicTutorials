import { Component, Input } from '@angular/core';
import { ConfigurationService } from '../../services/configuration.service';

@Component({
    selector: 'app',
    template: require('./app.component.html'),
    styles: [require('./app.component.css')]
})
export class AppComponent {
}

