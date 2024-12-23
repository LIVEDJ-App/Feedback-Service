import http from 'k6/http';
import { sleep } from 'k6';

export const options = {
  vus: 34,
  duration: '30s',
  cloud: {
    // LIVEDJ-Project
    projectID: 3727684,
    name: 'Feedback-Service Load Test'
  }
};

export default function() {
  http.get('http://34.27.47.102:8080/swagger/index.html');
  sleep(1);
}