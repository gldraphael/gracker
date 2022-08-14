# Setting up your local development environment

## Quickstart

Run:

```sh
docker compose pull
docker compose up -d
```

This will make the following services available:   

  Port | Service  
------:|---------
  17751 | RabbitMQ
  17752 | RabbitMQ management 
  17753 | PostgreSQL
  17754 | Adminer




## Detailed setup instructions

### Install pre-requisites

1. Install [Docker Desktop][docker-desktop-download]
1. Install & setup [git][git-installation-tutorial]
1. Install [VS Code][vs-code-download]
1. Optional, but recommended: Install [Visual Studio Community][vs-community-download] (free) or [Rider][rider-download] (free [for students][rider-education])

### Clone this repository

1. Open your terminal app
2. `cd` to the folder you keep your projects
    ```sh
    # I'd do something like this:
    cd projects

    # If you don't have a projects folder, 
    # you can create one first:
    mkdir projects
    # and then cd (change-directory) to it:
    cd projects

    # If you want to see where you are visually, run (include the .):
    start . # on windows to open windows explorer, or
    open .  # on macos to open finder
    ```
3. Run the following to "clone" (i.e. take a copy) of gracker's code:
    ```
    git clone https://github.com/gldraphael/gracker.git
    ```

### Next steps
Now that you've got the gracker code on your computer, you may:

1. run the [quickstart steps][quickstart] to play around with a running instance of Gracker
1. open the gracker.sln file with Visual Studio or Rider to review the code
1. open the project in VS Code and review the code: `code .`



[docker-desktop-download]: https://docs.docker.com/get-docker/
[git-installation-tutorial]: https://www.atlassian.com/git/tutorials/install-git
[vs-code-download]: https://code.visualstudio.com/download
[vs-community-download]: https://visualstudio.microsoft.com/vs/community/
[rider-download]: https://www.jetbrains.com/rider/
[rider-education]: https://www.jetbrains.com/community/education
[quickstart]: https://github.com/gldraphael/gracker/blob/main/README.md#quickstart
