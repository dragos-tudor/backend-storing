ARG DOTNET_SDK_VERSION=10.0.100-preview.5.25277.114

FROM quay.io/podman/stable:latest

RUN dnf update -y && \
		dnf install -y ca-certificates git curl nano

RUN curl -L https://aka.ms/install-dotnet-preview -o install-dotnet-preview.sh && \
		chmod +x install-dotnet-preview.sh && \
		./install-dotnet-preview.sh --version $DOTNET_SDK_VERSION && \
		rm ./install-dotnet-preview.sh

ENV SHELL="/bin/bash"

