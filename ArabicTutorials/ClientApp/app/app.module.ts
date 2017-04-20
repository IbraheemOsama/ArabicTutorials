import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { VideoQueryService } from './services/video.query.service';
import { ConfigurationService } from './services/configuration.service';
import { VideoListComponent } from './components/video/video.list.component';

@NgModule({
    bootstrap: [AppComponent],
    providers: [VideoQueryService, ConfigurationService],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        VideoListComponent,
        HomeComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'video', component: VideoListComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModule {
}
